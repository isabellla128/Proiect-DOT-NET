import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';

export default abstract class AbstractRestService<T> {
  constructor(
    private _http: HttpClient,
    private _url: string,
    private _collection$: BehaviorSubject<T[]>
  ) {}

  public get collection$(): BehaviorSubject<T[]> {
    return this._collection$;
  }

  getAll() {
    this._http.get<T[]>(this._url).subscribe(
      (result) => this.collection$.next(result),
      (error) => console.log(error)
    );
  }

  getOne(id: string) {
    return this._http.get<T>(this._url + '/' + id);
  }

  post(entity: T) {
    this._http.post<T>(this._url, entity).subscribe(
      (result) => this.collection$.next(this.addLocaly(result)),
      (error) => console.log(error)
    );
  }

  delete(entityId: string) {
    this._http.delete(this._url + '/' + entityId).subscribe(
      (result) => {
        this.deleteEntityLocaly(entityId);
      },
      (error) => console.log(error)
    );
  }

  update(entity: T) {
    this._http
      .put<T>(this._url + '/' + (entity as any)['id'], entity, {
        headers: {
          'Content-Type': 'application/json-patch+json',
        },
      })
      .subscribe(
        (result) => {
          this.updateLocaly(result);
        },
        (error) => console.error(error)
      );
  }

  private addLocaly(entity: T): T[] {
    const entities = this.collection$.getValue();
    entities.push(entity);
    return entities;
  }

  private deleteEntityLocaly(id: string) {
    this.collection$.next(
      this.collection$.getValue().filter((entity) => (entity as any).id !== id)
    );
  }

  private updateLocaly(entity: T) {
    const newArray = this.collection$.getValue();
    const index = newArray.findIndex(
      (elemnt) => (elemnt as any).id === (entity as any).id
    );
    newArray[index] = entity;
    this.collection$.next(newArray);
  }
}

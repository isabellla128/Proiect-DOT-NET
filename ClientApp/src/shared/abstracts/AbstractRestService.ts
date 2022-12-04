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

  post(entity: T) {
    this._http.post<T>(this._url, entity).subscribe(
      (result) => this.collection$.next(this.addLocaly(entity)),
      (error) => console.log(error)
    );
  }

  delete(entityId: string) {
    this.deleteEntityLocaly(entityId);
    this._http.delete(this._url + '/' + entityId).subscribe(
      (result) => console.log(result),
      (error) => console.log(error)
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
}

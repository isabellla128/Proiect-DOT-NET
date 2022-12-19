import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicsComponent } from './medics.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';

describe('MedicsComponent', () => {
  let component: MedicsComponent;
  let fixture: ComponentFixture<MedicsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [HttpClient],
      declarations: [MedicsComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(MedicsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

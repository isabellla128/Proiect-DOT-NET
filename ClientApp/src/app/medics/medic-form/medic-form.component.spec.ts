import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicFormComponent } from './medic-form.component';
import { FormBuilder } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';

describe('MedicFormComponent', () => {
  let component: MedicFormComponent;
  let fixture: ComponentFixture<MedicFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [FormBuilder, HttpClient],
      declarations: [MedicFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(MedicFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

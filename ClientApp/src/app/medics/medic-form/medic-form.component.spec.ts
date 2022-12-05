import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicFormComponent } from './medic-form.component';

describe('MedicFormComponent', () => {
  let component: MedicFormComponent;
  let fixture: ComponentFixture<MedicFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MedicFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MedicFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicScheduleComponent } from './medic-schedule.component';

describe('MedicScheduleComponent', () => {
  let component: MedicScheduleComponent;
  let fixture: ComponentFixture<MedicScheduleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MedicScheduleComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MedicScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

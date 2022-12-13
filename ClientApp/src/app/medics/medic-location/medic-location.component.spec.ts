import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicLocationComponent } from './medic-location.component';

describe('MedicLocationComponent', () => {
  let component: MedicLocationComponent;
  let fixture: ComponentFixture<MedicLocationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MedicLocationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MedicLocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

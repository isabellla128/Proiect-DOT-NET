import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicationsComponent } from './medications.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';

describe('MedicationsComponent', () => {
  let component: MedicationsComponent;
  let fixture: ComponentFixture<MedicationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [HttpClient],
      declarations: [MedicationsComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(MedicationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicProfileComponent } from './medic-profile.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

describe('MedicProfileComponent', () => {
  let component: MedicProfileComponent;
  let fixture: ComponentFixture<MedicProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [HttpClient, ActivatedRoute],
      declarations: [MedicProfileComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(MedicProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

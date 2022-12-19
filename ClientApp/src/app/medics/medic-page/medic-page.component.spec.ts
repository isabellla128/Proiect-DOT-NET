import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicPageComponent } from './medic-page.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

describe('MedicPageComponent', () => {
  let component: MedicPageComponent;
  let fixture: ComponentFixture<MedicPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [HttpClient, ActivatedRoute],
      declarations: [MedicPageComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(MedicPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

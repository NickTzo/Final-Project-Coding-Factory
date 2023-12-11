import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationReadComponent } from './reservation-read.component';

describe('ReservationReadComponent', () => {
  let component: ReservationReadComponent;
  let fixture: ComponentFixture<ReservationReadComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ReservationReadComponent]
    });
    fixture = TestBed.createComponent(ReservationReadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

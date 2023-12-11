import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReadCarComponent } from './read-car.component';

describe('ReadCarComponent', () => {
  let component: ReadCarComponent;
  let fixture: ComponentFixture<ReadCarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ReadCarComponent]
    });
    fixture = TestBed.createComponent(ReadCarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

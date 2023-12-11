import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListCarsComponent } from './list-cars.component';

describe('ListCarsComponent', () => {
  let component: ListCarsComponent;
  let fixture: ComponentFixture<ListCarsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ListCarsComponent]
    });
    fixture = TestBed.createComponent(ListCarsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

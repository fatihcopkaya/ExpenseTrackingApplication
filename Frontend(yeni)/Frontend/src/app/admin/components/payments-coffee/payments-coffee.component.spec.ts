import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentsCoffeeComponent } from './payments-coffee.component';

describe('PaymentsCoffeeComponent', () => {
  let component: PaymentsCoffeeComponent;
  let fixture: ComponentFixture<PaymentsCoffeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaymentsCoffeeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaymentsCoffeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

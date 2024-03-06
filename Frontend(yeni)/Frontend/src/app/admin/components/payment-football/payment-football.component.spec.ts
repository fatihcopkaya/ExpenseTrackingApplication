import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentFootballComponent } from './payment-football.component';

describe('PaymentFootballComponent', () => {
  let component: PaymentFootballComponent;
  let fixture: ComponentFixture<PaymentFootballComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaymentFootballComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaymentFootballComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

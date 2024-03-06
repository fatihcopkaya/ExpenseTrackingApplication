import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentInformationsComponent } from './payment-informations.component';

describe('PaymentInformationsComponent', () => {
  let component: PaymentInformationsComponent;
  let fixture: ComponentFixture<PaymentInformationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaymentInformationsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaymentInformationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserinfomationCoffeeComponent } from './userinfomation-coffee.component';

describe('UserinfomationCoffeeComponent', () => {
  let component: UserinfomationCoffeeComponent;
  let fixture: ComponentFixture<UserinfomationCoffeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserinfomationCoffeeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserinfomationCoffeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

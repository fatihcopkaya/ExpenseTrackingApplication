import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserInformationFootballComponent } from './user-information-football.component';

describe('UserInformationFootballComponent', () => {
  let component: UserInformationFootballComponent;
  let fixture: ComponentFixture<UserInformationFootballComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserInformationFootballComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserInformationFootballComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

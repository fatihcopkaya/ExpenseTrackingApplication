import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AnonymousLayoutComponent } from './layout.component';

describe('AnonymousLayoutComponent', () => {
  let component: AnonymousLayoutComponent;
  let fixture: ComponentFixture<AnonymousLayoutComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AnonymousLayoutComponent]
    });
    fixture = TestBed.createComponent(AnonymousLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
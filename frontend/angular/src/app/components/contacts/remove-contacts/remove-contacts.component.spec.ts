import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoveContactsComponent } from './remove-contacts.component';

describe('RemoveContactsComponent', () => {
  let component: RemoveContactsComponent;
  let fixture: ComponentFixture<RemoveContactsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RemoveContactsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RemoveContactsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

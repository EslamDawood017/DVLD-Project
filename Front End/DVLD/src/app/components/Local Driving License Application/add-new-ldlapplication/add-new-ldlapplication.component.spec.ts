import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNewLDLApplicationComponent } from './add-new-ldlapplication.component';

describe('AddNewLDLApplicationComponent', () => {
  let component: AddNewLDLApplicationComponent;
  let fixture: ComponentFixture<AddNewLDLApplicationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddNewLDLApplicationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddNewLDLApplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

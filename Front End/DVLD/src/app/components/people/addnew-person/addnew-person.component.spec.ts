import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddnewPersonComponent } from './addnew-person.component';

describe('AddnewPersonComponent', () => {
  let component: AddnewPersonComponent;
  let fixture: ComponentFixture<AddnewPersonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddnewPersonComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddnewPersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

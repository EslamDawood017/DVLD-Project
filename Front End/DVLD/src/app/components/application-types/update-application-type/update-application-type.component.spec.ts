import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateApplicationTypeComponent } from './update-application-type.component';

describe('UpdateApplicationTypeComponent', () => {
  let component: UpdateApplicationTypeComponent;
  let fixture: ComponentFixture<UpdateApplicationTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateApplicationTypeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateApplicationTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

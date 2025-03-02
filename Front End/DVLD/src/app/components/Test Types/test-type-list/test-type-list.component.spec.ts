import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestTypeListComponent } from './test-type-list.component';

describe('TestTypeListComponent', () => {
  let component: TestTypeListComponent;
  let fixture: ComponentFixture<TestTypeListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TestTypeListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TestTypeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

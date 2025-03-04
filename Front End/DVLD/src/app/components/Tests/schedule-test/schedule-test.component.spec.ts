import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleTestComponent } from './schedule-test.component';

describe('ScheduleTestComponent', () => {
  let component: ScheduleTestComponent;
  let fixture: ComponentFixture<ScheduleTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScheduleTestComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScheduleTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

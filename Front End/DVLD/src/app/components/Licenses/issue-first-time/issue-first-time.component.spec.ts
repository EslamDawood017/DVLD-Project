import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IssueFirstTimeComponent } from './issue-first-time.component';

describe('IssueFirstTimeComponent', () => {
  let component: IssueFirstTimeComponent;
  let fixture: ComponentFixture<IssueFirstTimeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IssueFirstTimeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IssueFirstTimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

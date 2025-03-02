import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetainListComponent } from './detain-list.component';

describe('DetainListComponent', () => {
  let component: DetainListComponent;
  let fixture: ComponentFixture<DetainListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetainListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetainListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

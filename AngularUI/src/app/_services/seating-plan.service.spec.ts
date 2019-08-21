import { TestBed } from '@angular/core/testing';

import { SeatingPlanService } from './seating-plan.service';

describe('SeatingPlanService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SeatingPlanService = TestBed.get(SeatingPlanService);
    expect(service).toBeTruthy();
  });
});

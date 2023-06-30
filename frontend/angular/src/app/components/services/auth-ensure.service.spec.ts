import { TestBed } from '@angular/core/testing';

import { AuthEnsureService } from './auth-ensure.service';

describe('AuthEnsureService', () => {
  let service: AuthEnsureService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthEnsureService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

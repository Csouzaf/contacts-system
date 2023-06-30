import { TestBed } from '@angular/core/testing';

import { AuthRegisterService } from './auth-register.service';

describe('AuthRegisterService', () => {
  let service: AuthRegisterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthRegisterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { Injectable } from '@angular/core';

@Injectable()
export class AdalConfig {
  instance: string;
  tenant: string;
  redirectUri: string;
  cacheLocation?: 'localStorage' | 'sessionStorage';
  clientId: string;
  postLogoutRedirectUri: string;
  endpoints: any;
}

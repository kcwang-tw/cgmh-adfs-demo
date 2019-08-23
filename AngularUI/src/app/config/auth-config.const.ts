import { AuthConfig } from 'angular-oauth2-oidc';

export const authConfig: AuthConfig = {
  issuer: 'https://cghadfs.cgmh.org.tw/adfs/',
  loginUrl: 'https://cghadfs.cgmh.org.tw/adfs/oauth2/authorize',
  redirectUri: window.location.origin + '/seats',
  clientId: '4200',
  scope: 'openid profile'
  // issuer: 'https://adfs2016.southeastasia.cloudapp.azure.com/adfs/',
  // loginUrl: 'https://adfs2016.southeastasia.cloudapp.azure.com/adfs/oauth2/authorize',
  // redirectUri: window.location.origin + '/seats',
  // clientId: 'e1cf1ac7-0462-4b48-bc99-b8d425a2d2e3',
  // scope: 'openid profile'
};

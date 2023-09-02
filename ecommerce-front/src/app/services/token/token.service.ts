import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  constructor() {}

  public getUserIdFromToken() {
    let jwt = localStorage.getItem('acess_token');

    if (jwt) {
      const decodedToken = this.decodeJwt(jwt);
      console.log('Informa��es do usu�rio do JWT:', decodedToken);
      return decodedToken;
    } else {
      // JWT n�o encontrado no Local Storage
      console.log('JWT n�o encontrado no Local Storage');
    }
  }

  private decodeJwt(jwt: string): any {
    const tokenParts = jwt.split('.');
    if (tokenParts.length !== 3) {
      throw new Error('Token JWT inv�lido');
    }

    // Decodifique a parte de carga do JWT (segunda parte)
    const payload = atob(tokenParts[1]);
    return JSON.parse(payload);
  }
}

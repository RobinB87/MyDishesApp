import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { Observable, of } from 'rxjs';
import { catchError, map, switchMap, tap } from 'rxjs/operators';
import { AuthService } from '../../services/auth.service';
import {
  EAuthActionTypes,
  LogIn,
  LogInFailure,
  LogInSuccess,
} from './auth.actions';

@Injectable()
export class AuthEffects {
  constructor(
    private actions$: Actions,
    private authService: AuthService,
    private router: Router
  ) {}

  @Effect()
  LogIn: Observable<any> = this.actions$.pipe(
    ofType(EAuthActionTypes.LOGIN),
    map((action: LogIn) => action.payload),
    switchMap((payload) => {
      return (
        this.authService.logIn(payload.email, payload.password).pipe(
          map((user) => {
            console.log(user);
            return new LogInSuccess({
              token: user.token,
              email: payload.email,
            });
          })
        ),
        catchError((error) => {
          console.log(error);
          return of(new LogInFailure({ error: error }));
        })
      );
    })
  );

  @Effect({ dispatch: false })
  LogInSuccess: Observable<any> = this.actions$.pipe(
    ofType(EAuthActionTypes.LOGIN_SUCCESS),
    tap((user) => {
      localStorage.setItem('token', user.payload.token);
      this.router.navigateByUrl('/');
    })
  );

  @Effect({ dispatch: false })
  LogInFailure: Observable<any> = this.actions$.pipe(
    ofType(EAuthActionTypes.LOGIN_FAILURE)
  );
}

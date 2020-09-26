import { Action } from '@ngrx/store';

export enum EAuthActionTypes {
  LOGIN = '[Auth] Login',
  LOGIN_SUCCESS = '[Auth] Login Success',
  LOGIN_FAILURE = '[Auth] Login Failure',

  GET_STATUS = '[Auth] GetStatus',
}

export class LogIn implements Action {
  readonly type = EAuthActionTypes.LOGIN;
  constructor(public payload: any) {}
}

export class LogInSuccess implements Action {
  readonly type = EAuthActionTypes.LOGIN_SUCCESS;
  constructor(public payload: any) {}
}

export class LogInFailure implements Action {
  readonly type = EAuthActionTypes.LOGIN_FAILURE;
  constructor(public payload: any) {}
}

export class GetStatus implements Action {
  readonly type = EAuthActionTypes.GET_STATUS;
}

export type All = LogIn | LogInSuccess | LogInFailure | GetStatus;

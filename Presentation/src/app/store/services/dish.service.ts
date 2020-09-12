import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, delay } from 'rxjs/operators';
import { Dish } from '../../core/models/dish.model';
import { api, DataServiceError, fakeDelays } from '../config';

@Injectable()
export class DishService {
  constructor(private http: HttpClient) {}

  getDishes(): Observable<Dish[]> {
    return this.http
      .get<Array<Dish>>(`${api}/dish`)
      .pipe(delay(fakeDelays.select), catchError(this.handleError()));
  }

  private handleError<T>(requestData?: T) {
    return (res: HttpErrorResponse) => {
      const error = new DataServiceError(res.error, requestData);
      console.error(error);
      // return new ErrorObservable(error);
      return throwError(error);
    };
  }
}

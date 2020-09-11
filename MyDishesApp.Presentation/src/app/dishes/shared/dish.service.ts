import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { Dish } from './dish.model';
import { BaseService } from '../../shared/base.service';
import { Operation } from 'fast-json-patch';


@Injectable()
export class DishService extends BaseService {

  httpClient: any;
  // asyncResult: any;

  constructor(private http: HttpClient) {
    super();
  }

  // ENTERNE LINKS NAAR API

  // Get
  getDishes(): Observable<Dish[]> {
    return this.http.get<Dish[]>(`${this.apiUrl}/dishes`);
  }
 
  getDish(dishId: number): Observable<Dish> {
    return this.http.get<Dish>(`${this.apiUrl}/dishes/${dishId}`);
  }

  // Add
  addDish(dishToAdd: Dish): Observable<Dish> {
      return this.http.post<Dish>(`${this.apiUrl}/dishes`, dishToAdd)
  }

  // Update (send patchdocument to API)
  partiallyUpdateDish(dishId: number, patchDocument: Operation[]): Observable<any> {
    return this.http.patch(`${this.apiUrl}/dishes/${dishId}`, patchDocument,
      { headers: { 'Content-Type': 'application/json-patch+json' } });
  }

  // Delete
  deleteDish(dishId: number): Observable<Dish> {
    return this.http.delete<Dish>(`${this.apiUrl}/dishes/${dishId}`);
  }
}

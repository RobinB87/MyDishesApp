import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Dish } from '../models';

@Injectable({
  providedIn: 'root',
})
export class DishServiceService {
  apiUrl = environment.tempJsonApi;

  constructor(private http: HttpClient) {}

  /** Service call to get all dishes */
  public getAll(): Observable<Dish[]> {
    return this.http.get<Dish[]>(`${this.apiUrl}/`);
  }

  /** Service call to create a dish */
  addDish(dishToAdd: Dish): Observable<Dish> {
    return this.http.post<Dish>(`${this.apiUrl}/dish`, dishToAdd);
  }

  /** Service call to delete a dish */
  deleteDish(dishId: number): Observable<Dish> {
    return this.http.delete<Dish>(`${this.apiUrl}/dish/${dishId}`);
  }
}

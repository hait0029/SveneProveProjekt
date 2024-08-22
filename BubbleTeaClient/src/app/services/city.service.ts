import { Injectable } from '@angular/core';
import { environment } from '../../environments/environments';
import { HttpClient } from '@angular/common/http';



@Injectable({
  providedIn: 'root'
})
export class CityService {
  private apiUrl= environment.apiurl + "city";
  constructor(private http: HttpClient) { }
}

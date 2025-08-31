import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
interface AuthUser {
  name: string;
  email: string;
  password: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public forecasts: WeatherForecast[] = [];
  public uiVariable!: string;
  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getForecasts();
    this.getAuthTest();
  }

  getForecasts() {
    this.http.get<WeatherForecast[]>('/weatherforecast').subscribe(
      (result) => {
        this.forecasts = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }
  getAuthTest() {
    this.http.get('auth/test', { responseType: 'text' }).subscribe(data => {
      this.uiVariable = data;
    });
  }

  title = 'angularauthtest.client';
}

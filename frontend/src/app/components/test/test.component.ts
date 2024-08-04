import { Component, inject } from '@angular/core';
import { NgIf } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'test',
  standalone: true,
  imports: [HttpClientModule, NgIf],
  templateUrl: './test.component.html',
  styleUrl: './test.component.css',
})
export class TestComponent {
  data: any = {};
  private url: string = 'http://localhost:5286/api/testController/getString';

  //constructor(private http: HttpClient) {}

  httpClient = inject(HttpClient);
  ngOnInit(): void {
    this.httpClient.get(this.url).subscribe((data) => {
      console.log(data);
      this.data = data;
    });
  }
}

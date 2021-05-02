import { Component, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr'; 

@Component({
  selector: 'app-time',
  templateUrl: './time.component.html',
  styleUrls: ['./time.component.css']
})
export class TimeComponent implements OnInit {

  hubConnection: signalR.HubConnection;
  time: TimeModel;
  title: string;

  constructor() {
    this.title = "Current time:"
  }

  ngOnInit(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5000/time")
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))

    this.hubConnection.on('currentTime', (time) => { this.time = time; });
  }

  ngOnDestroy(): void {
    this.hubConnection
      .stop()
      .then(() => console.log('Connection closed'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }
}

class TimeModel {
  hours: number;
  minutes: number;
  seconds: number;
}

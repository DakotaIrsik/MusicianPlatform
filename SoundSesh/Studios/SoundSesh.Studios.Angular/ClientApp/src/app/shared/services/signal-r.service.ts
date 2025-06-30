import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { environment } from 'src/environments/environment';
import { ChatModel } from '../models/chat';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public data: ChatModel[] = [];
  public bradcastedData: ChatModel[];

private hubConnection: signalR.HubConnection;
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl( environment.apiUrl.replace("api/v1/","") + 'chat')
                            .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public addTransferChartDataListener() {

    this.hubConnection.on('transferchartdata', (data) => {
      if (this.data.length >= 10) {
        this.data.shift();
      }
      this.data.push(data);
      console.log(data);
    });
  }

  public broadcastChartData = () => {
    this.hubConnection.invoke('broadcastchartdata', this.data)
    .catch(err => console.error(err));
  }

  public addBroadcastChartDataListener = () => {
    this.hubConnection.on('broadcastchartdata', (data) => {
      this.bradcastedData = data;
    })
  }
}



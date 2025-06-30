import { Component, OnInit } from '@angular/core';
import { FeedbackService } from 'src/app/shared/services/feedback.service';
import { Feedback } from 'src/app/shared/models/feedback';
import { TabulatorComponent } from 'src/app/shared/tabulator/tabulator.component';

@Component({
  selector: 'studio-portal-feature-requests',
  templateUrl: './feature-requests.component.html',
  styleUrls: ['./feature-requests.component.scss'],
  providers: [TabulatorComponent]
})
export class FeatureRequestsComponent implements OnInit {
  public columns = [
    { title: 'Item #', field: 'workitem', width: '150', formatter: 'html' },
    { title: 'Importance', field: 'importance', width: '150' },
    { title: 'Pay for Feature?', field: 'willingtopayforfeature', width: '170' },
    { title: 'Feature Price', field: 'willingtopayforfeatureamount', width: '140'  },
    { title: 'Pay Subscription?', field: 'willingtopayforsubscription', width: '175'  },
    { title: 'Subscription price?', field: 'willingtopayforsubscriptionamount', width: '180'  },
    { title: 'Comment', field: 'comments', width: '150' },
    { title: 'Created On', field: 'createdate', width: '250' },
    { title: 'User', field: 'userid', width: '300' }

  ];

  constructor(private feedbackService: FeedbackService, private tableComponent: TabulatorComponent) { }



  feedback: Feedback[];

  ngOnInit() {
    this.loadFeedback();
  }



  loadFeedback() {
    this.feedbackService.search().subscribe(
      response => {
        this.tableComponent.columnNames = this.columns;
        this.tableComponent.tableData = response.data;
        
        this.tableComponent.drawTable();
      }
    );
  }
}

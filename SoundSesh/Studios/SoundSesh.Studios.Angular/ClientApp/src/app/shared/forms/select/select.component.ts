import { Component, OnInit, Input } from '@angular/core';
import { LookUpService } from '../../services/lookup.service';
import { MdbSelect } from '../../models/mdb-select';
import { LookUpSelectionService } from '../../services/lookup-selection.service';
import { LookUpRelation } from '../../models/lookup-relation';

@Component({
  selector: 'studio-portal-select',
  templateUrl: './select.component.html'
})
export class SelectFromApiComponent implements OnInit {
  @Input() selectedValues: string[];
  @Input() api: string;
  @Input() key: string;
  @Input() placeholder: string;
  @Input() multiple: boolean;

  selects: Array<any>;

  constructor(protected lookUpService: LookUpService, protected lookUpSelectionService: LookUpSelectionService) { }

  ngOnInit() {
    this.lookUpService.get(this.api).subscribe(response => {
      this.selects = response.map(data => new MdbSelect(data.name, data.name));
      if (this.selectedValues) {
        this.getSelectedValues(this.selectedValues);
      }
    });

  }

  getSelectedValues(event: any) {
    const x = (Array.isArray(event)) ? event.map(e => e) : [event];
    this.lookUpSelectionService.addLookUp(new LookUpRelation(this.key, this.selects.filter(s => x.indexOf(s.label) > -1)));
  }
}

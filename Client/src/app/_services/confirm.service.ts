import { Injectable } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { ConfirmDialogComponent } from '../modals/confirm-dialog/confirm-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class ConfirmService {
  bsModelref: BsModalRef;
  constructor(private modalService: BsModalService) { }

  confirm(title = 'Confirmation', message = 'Are you sure you want to do this?', btnOkText = 'Ok', btnCancelText = 'Cancel'): Observable<boolean> {
    const  config = {
      initialState: {
        title,
        message,
        btnOkText,
        btnCancelText
      }
    }

    this.bsModelref = this.modalService.show(ConfirmDialogComponent, config);

    return new Observable<boolean>(this.getResult());
  }

  private getResult() {
    return (observer) => {
        const subscription = this.bsModelref.onHidden.subscribe(() => {
          observer.next(this.bsModelref.content.result);
          observer.complete();
      });

      return {
        unsubscribe() {
          subscription.unsubscribe();
        }
      }
    }
  }
}

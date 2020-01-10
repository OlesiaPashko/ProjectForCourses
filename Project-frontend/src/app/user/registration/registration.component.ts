import { Component, OnInit } from '@angular/core';
import {UserService} from './../../shared/user.service'
import {ToastrService} from 'ngx-toastr'
@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: []
})
export class RegistrationComponent implements OnInit {

  constructor(public service: UserService, private toastr:ToastrService) { }

  ngOnInit() {
  }

  onSubmit(){
  	this.service.register()
         .subscribe(
         	/*(result)  => {
         	if(result){
         		localStorage.setItem('Bearer', result.token);
         		console.log(result);}*/
         (res: any) => {
          console.log(res);
        if (res.success) {
          this.service.formModel.reset();
          localStorage.setItem('Bearer', res.token);
          console.log(res);
          this.toastr.success('New user created!', 'Registration successful.');
        } else {
          console.log(res);
          this.toastr.error('element.description','Registration failed.');

          res.error.errors.forEach(element => {
            switch (element.code) {
              case 'DuplicateUserName':
                this.toastr.error('Username is already taken','Registration failed.');
                console.log('Username is already taken','Registration failed.');
                break;

              default:
              this.toastr.error(element.description,'Registration failed.');
              console.log(element.description)
                break;
            }
          });
        }
      },
      err => {
        console.log(err);
      }
    );
         //}});
  }

}

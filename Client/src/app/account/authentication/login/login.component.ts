import { Component } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, finalize, map, switchMap, take } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  getDate = new Date().getFullYear();
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email], [this.validateEmailExists()]),
    password: new FormControl('', Validators.required)
  })
  returnUrl: string;
  validationErrors: string[] | undefined;

  constructor(private accountService: AccountService, private router: Router,
    private activatedRoute: ActivatedRoute) {
      this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/home'
    }

  onSubmit() {
    this.accountService.login(this.loginForm.value).subscribe({
      next: () => this.router.navigateByUrl(this.returnUrl),
      error: error => {
        this.validationErrors = error
      }
    })
  }

  validateEmailExists(): AsyncValidatorFn {
    return (control: AbstractControl) => {
      return control.valueChanges.pipe(
        debounceTime(1000), // Wait for 1 second after the user has stopped typing
        take(1),
        switchMap(() => {
          // Call the checkEmailExists method of the account service to perform the validation check
          return this.accountService.checkEmailExists(control.value).pipe(
            map(result => result ? null : { emailNotExists: true }),
            finalize(() => control.markAsTouched())
          )
        })
      )
    }
  }

}

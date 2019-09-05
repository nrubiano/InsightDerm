import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'app/services/authentication.service';
declare var $: any;
declare var jQuery: any;

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    providers: [AuthenticationService]
})

export class LoginComponent implements OnInit {

    username: string;
    password: string;

    constructor(private _authenticationService: AuthenticationService) {

    }

    ngOnInit() {
        $(function() {
            // Form Validation
            $('#form-validation').validate({
                submit: {
                    settings: {
                        inputContainer: '.form-group',
                        errorListClass: 'form-control-error',
                        errorClass: 'has-danger'
                    }
                }
            });

            // Show/Hide Password
            $('.password').password({
                eyeClass: '',
                eyeOpenClass: 'icmn-eye',
                eyeCloseClass: 'icmn-eye-blocked'
            });

            // Switch to fullscreen
            $('.switch-to-fullscreen').on('click', function () {
                $('.cat__pages__login').toggleClass('cat__pages__login--fullscreen');
            })

            // Change BG
            $('.random-bg-image').on('click', function () {
                let min = 1, max = 5,
                    next = Math.floor($('.random-bg-image').data('img')) + 1,
                    final = next > max ? min : next;

                $('.random-bg-image').data('img', final);
                $('.cat__pages__login').data('img', final).css('backgroundImage', 'url(/assets/modules/pages/img/login/' + final + '.jpg)');
            })

        });
    }

    login() {
        console.log("from login");
        this._authenticationService.login(this.username, this.password)
                .toPromise()
                .then(res => {
                    console.log(res);
                })
                .catch(e => {
                    console.log("something went very bad", e);
                });
    }
}


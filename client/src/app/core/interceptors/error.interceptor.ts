import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Router, NavigationExtras } from '@angular/router';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private router: Router, private toastr: ToastrService) {}
    // intercept http request
    // req - outgoing reqest
    // next - http response that is coming back
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError(error => {
                if (error) {
                    if (error.status === 400) {
                        if (error.error.errors) {
                            throw error.error;
                        } else{
                            this.toastr.error(error.error.message, error.error.statusCode);
                        }
                    }

                    if (error.status === 401) {
                        this.toastr.error(error.error.message, error.error.statusCode);
                    }

                    if (error.status === 404) {
                        this.router.navigateByUrl('/not-found');
                    }

                    if (error.status === 500) {
                        // error.error is in the exception messags you get from api
                        const navigationExtras: NavigationExtras = {state: {error: error.error}};
                        this.router.navigateByUrl('/server-error', navigationExtras);
                    }
                }

                return throwError(error);
            })
        );
    }

}

import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from "@angular/router";
import { AuthServiceService } from "../services/Auth/auth-service.service";


@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {


    constructor(private router : Router , 
        private authService : AuthServiceService){}

        
    canActivate(): boolean {
        if (this.authService.isAuthenticated()) {
            return true;
          } else {
            this.router.navigate(['/login']);
            return false;
          }
    }
        

}
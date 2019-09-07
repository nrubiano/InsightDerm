import {Directive, Input, OnInit, TemplateRef, ViewContainerRef} from "@angular/core";
import {AuthenticationService} from "../services/authentication.service";

@Directive({
    selector: '[appHasRole]'
})
export class HasRoleDirective implements OnInit {
    // the role the user must have
    @Input() appHasRole: string[];

    isVisible = false;

    /**
     * @param {ViewContainerRef} viewContainerRef
     * 	-- the location where we need to render the templateRef
     * @param {TemplateRef<any>} templateRef
     *   -- the templateRef to be potentially rendered
     * @param {AuthenticationService} authService
     *   -- will give us access to the roles a user has
     */
    constructor(
        private viewContainerRef: ViewContainerRef,
        private templateRef: TemplateRef<any>,
        private authService: AuthenticationService
    ) {}

    ngOnInit() {
        //  We subscribe to the roles$ to know the roles the user has
        const currentUser = this.authService.getCurrentUser();

        // If he doesn't have any roles, we clear the viewContainerRef
        if (!currentUser) {
            this.viewContainerRef.clear();
            return;
        }

        // If the user has the role needed to
        // render this component we can add it
        if (this.appHasRole.includes(currentUser.role.toLowerCase())) {
            if (!this.isVisible) {
                // We update the `isVisible` property and add the
                // templateRef to the view using the
                // 'createEmbeddedView' method of the viewContainerRef
                this.isVisible = true;
                this.viewContainerRef.createEmbeddedView(this.templateRef);
            }
        } else {
            // If the user does not have the role,
            // we update the `isVisible` property and clear
            // the contents of the viewContainerRef
            this.isVisible = false;
            this.viewContainerRef.clear();
        }

    }
}

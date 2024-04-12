import jQuery from "jquery";

// these are not referenced directly, but do not remove! They are required
import inputmask from "inputmask";

import { initAll } from 'govuk-frontend';

// @ts-ignore
window.$ = window.jQuery = jQuery;


import addNewRow from "./common/AddNewRow";

// overrides of government scripts
import Accordian from "./common/overrides"

// add imports to further files here

$(document).ready(function () {
    initAll({
        characterCount: {
            i18n: { ...resources.characterCount }
        },
        accordion: {
            i18n: { ...resources.accordian }
        }
    });

    var inputs = $('input.3dp');

    // setup input masks for 3 decimal places
    Inputmask({
        regex: "^[0-9]*(\\.[0-9]{0,3})?$",
        placeholder: ""
    }).mask(inputs);
});

export {
    //Test
};
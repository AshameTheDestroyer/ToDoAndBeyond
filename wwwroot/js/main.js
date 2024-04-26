/**
 * @param {HTMLButtonElement} button 
 */
function OnSearchButtonClick(button) {
    const input = button.closest("header").querySelector("input");
    const ACTIVE_SEARCH_INPUT_CLASS = "active-search-input";

    if (input.classList.contains(ACTIVE_SEARCH_INPUT_CLASS)) {
        button.closest("header").querySelector("a").click();
        return;
    }

    function CloseSearchInput() {
        input.classList.remove(ACTIVE_SEARCH_INPUT_CLASS);
        input.removeEventListener("blur", OnSearchInputBlur);
        input.removeEventListener("keydown", OnSearchInputKeydown);
    }

    function OnSearchInputBlur() {
        if (input.value == "") { CloseSearchInput(); }
    }

    /**
     * @param {KeyboardEvent} e 
     */
    function OnSearchInputKeydown(e) {
        switch (e.key) {
            case "Escape": CloseSearchInput(); break;
            case "Enter": input.blur(); setTimeout(() => button.click(), 10); break;
        }
    }

    input.focus();
    input.classList.add(ACTIVE_SEARCH_INPUT_CLASS);
    input.addEventListener("blur", OnSearchInputBlur);
    input.addEventListener("keydown", OnSearchInputKeydown);
}
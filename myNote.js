toggle: function(showing) {
        debugger;
        var _this8 = this;
        showing = void 0 === showing ? !this.option("visible") : showing;
        var result = new _deferred.Deferred;
        if (showing === this.option("visible")) {
            return result.resolveWith(this, [showing]).promise()
        }
        var animateDeferred = new _deferred.Deferred;
        this._animateDeferred = animateDeferred;
        this.option("visible", showing);
        animateDeferred.promise().done((function() {
            delete _this8._animateDeferred;
            result.resolveWith(_this8, [_this8.option("visible")])
        }));
        return result.promise()
    },

    _toggleVisibility: function(visible) {
        debugger;
        this.$element().toggleClass("dx-state-invisible", !visible);
        this.setAria("hidden", !visible || void 0)
    },


    //line 158377 select all
    _preventNestedFocusEvent: function(event) {
        debugger;
        if (event.isDefaultPrevented()) {
            return true
        }
        var result = this._isNestedTarget(event.relatedTarget);
        if ("focusin" === event.type) {
            result = result && this._isNestedTarget(event.target) && !this._isInput(event.target)
        }
        result && event.preventDefault();
        return result
    },

    _setFocusedItem: function($target) {
        debugger;
        if (!$target || !$target.length) {
            return
        }
        this._updateFocusedItemState($target, true);
        this.onFocusedItemChanged(this.getFocusedItemId());
        if (this.option("selectOnFocus")) {
            this._selectFocusedItem($target)
        }
    },
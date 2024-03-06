
export class BaseResult {
    constructor() { 
    }
    isSuccess!: boolean
    errors!: []
}

export class BaseResultData<T> extends BaseResult {
    
    constructor() {
        super();
        
    }
    data!: T
}


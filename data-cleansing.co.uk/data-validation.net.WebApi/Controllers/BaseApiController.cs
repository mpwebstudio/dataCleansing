namespace data_validation.net.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    using data_cleansing.net.Data;
    
    public class BaseApiController : ApiController
    {
        protected IUowData Data;

        public BaseApiController(IUowData data)
        {
            this.Data = data;
        }

        public BaseApiController()
            : this(new UowData())
        {

        }
    }
}
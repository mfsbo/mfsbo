<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cybersource-payment.aspx.cs" Inherits="BookingDotcom.cybersource_payment" %>

<!DOCTYPE html>

<html lang="en" runat="server">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Pay Online With Cybersource</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.1/jquery.min.js" integrity="sha512-aVKKRRi/Q/YV+4mjoKBsE4x3H+BkegoM/em46NNlCqNTmUYADjBbeNefNxYV7giUp0VxICtqdrbqU7iVaeZNXA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="https://flex.cybersource.com/cybersource/assets/microform/0.11/flex-microform.min.js"></script>
    <style>
        #number-container, #securityCode-container {
            height: 38px;
        }

        .flex-microform-focused {
            background-color: #fff;
            border-color: #80bdff;
            outline: 0;
            box-shadow: 0 0 0 0.2rem rgba(0,123,255,.25);
        }

        .card {
            margin-top: 5%;
        }

        .errorMessage {
            background: #f59d9d;
        }
    </style>
</head>
<body>
    <script type="text/javascript">        

        function callfromIFrameError(errorMessage) {

            $("#errorsDetails").html(errorMessage);
            $("#errorsDetails").show();
            $("#cardcontainer").hide();
            $("#paymentdev").hide();
            $(window).scrollTop($('#errorsDetails').offset().top);
        }

        function callfromIFrame(resp) {

            //no need of this commented code as micrform javascript is taking care of it
            //just in case anyting change from javascrpt side keeping it handy
            //$("#my-sample-form").get(0).reset()
            //var reformhtml = '<label for="cardholderName">Name</label>' +
            //    '<input id="cardholderName" class="form-control" name="cardholderName" placeholder="Name on the card">' +
            //    '<label id="cardNumber-label">Card Number</label>' +
            //    '<div id="number-container" class="form-control"></div>' +
            //    '<label for="securityCode-container">Security Code</label>' +
            //    '<div id="securityCode-container" class="form-control"></div>';
            //$("#carddiv").html(reformhtml);

            var reformpaymenthtml = '<iframe name="my_iframe" class="form-control" ></iframe>' +
                '<form id="paymentconfirmation" action="' + checkoutAPIUrl + '" method="post" target="my_iframe">' +
                '<div id="UnsignedDataSection" class="section" style="display:none;">' +
                '<span>capture_context:</span><input id="capturecontext" type="text" name="capture_context"><br />' +
                '<span>transient_token:</span><input id="transienttoken" type="text" name="transient_token"><br />' +
                '</div>' +
                '</form>';

            $("#paymentdev").html(reformpaymenthtml);
            $("#cardcontainer").hide();
            $("#paymentdev").hide();

            if (resp && resp.decision) {
                if (resp.decision === "ACCEPT" && resp.decision_reason_code === "100") {
                    $("#messagepannel").css("background", "#98eeab");
                    $("#messagepannel").show();
                    $("#messagepannel").html(resp.message);
                    //hide re-card payment on ACCEPT as transection_uuid will be same or generate transection_uuid server side.
                    $("#Continue").hide();
                } else {
                    $("#messagepannel").css("background", "#f59d9d");
                    $("#messagepannel").show();
                    $("#messagepannel").html(resp.message);
                }
            } else {
                $("#messagepannel").css("background", "#f59d9d");
                $("#messagepannel").show();
                $("#messagepannel").html("Invalid data passed from server");
            }
            $(window).scrollTop($('#messagepannel').offset().top);
        }

        function btnclick() {

            $("#errorsDetails").hide();
            $("#messagepannel").hide();
            
            var errorObject = new Array();
            if (!$("#Forename").val()) { errorObject.push("You must enter the billing Forename"); }
            if (!$("#Surname").val()) { errorObject.push("You must enter the billing Surname"); }
            if (!$("#AddressLine1").val()) { errorObject.push("You must enter the billing AddressLine1"); }
            if (!$("#City").val()) { errorObject.push("You must enter the billing City"); }
            if (!$("#Country").val()) { errorObject.push("You must enter the billing Country"); }
            if (!$("#Postcode").val()) { errorObject.push("You must enter the billing Postcode"); }
            else {
                if ($("#Postcode").val().length > 10) {
                    errorObject.push("Postcode should at max be 10 character");
                }
            }

            if (!$("#State").val()) { errorObject.push("You must enter the billing State"); }
            else {
                if ($("#State").val().length != 2) {
                    errorObject.push("State should be 2 character");
                }
            }

            if (!$("#PhoneNumber").val()) { errorObject.push("You must enter the Phone Number"); }

            if (!$("#EmailAddress").val()) {
                errorObject.push("You must enter the billing EmailAddress");
            } else {
                var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (!regex.test($("#EmailAddress").val())) {
                    errorObject.push("EmailAddress not in correct format");
                }
            }

            if (errorObject.length > 0) {
                errorMessage = "";
                $.each(errorObject, function (key, value) {
                    errorMessage = errorMessage + value + "<br/>";
                });

                $("#messagepannel").css("background", "#f59d9d");
                $("#messagepannel").show();
                $("#messagepannel").html(errorMessage);
                $(window).scrollTop($('#messagepannel').offset().top);
                return false;
            }
            return true;
        }

        // custom styles that will be applied to each field we create using Microform
        var myStyles = {
            'input': {
                'font-size': '14px',
                'font-family': 'helvetica, tahoma, calibri, sans-serif',
                'color': '#555'
            },
            ':focus': { 'color': 'blue' },
            ':disabled': { 'cursor': 'not-allowed' },
            'valid': { 'color': '#3c763d' },
            'invalid': { 'color': '#a94442' }
        };

        // setup
        var flex;
        var microform;
        var captureContext1;
        var checkoutAPIUrl = '<%= ViewState["checkoutPaymentUrl"] %>';       

        function GenerateMicroForm(captureContext) {

            $(document).ready(function () {
                captureContext1 = captureContext;
                flex = new Flex(captureContext);
                microform = flex.microform({ styles: myStyles });
                var number = microform.createField('number', { placeholder: 'Enter card number' });
                var securityCode = microform.createField('securityCode', { placeholder: '•••' });

                number.load($("#number-container").get(0));
                securityCode.load($('#securityCode-container').get(0));
                $("#cardcontainer").show();
                $(window).scrollTop($('#cardcontainer').offset().top);

            });
        }

        function payclick() {
            $("#errors-output").hide();
            var options = {
                expirationMonth: $("#expMonth").val(),
                expirationYear: $("#expYear").val()
            };

            microform.createToken(options, function (err, token) {
                if (err) {
                    //handle error                    
                    var errorObject = new Array();
                    $.each(err.details, function (key, value) {
                        errorObject.push("At location: " + value.location + " error: " + value.message);
                    });
                    if (errorObject.length > 0) {
                        errorMessage = "";
                        $.each(errorObject, function (key, value) {
                            errorMessage = errorMessage + value + "<br/>";
                        });
                        $("#errors-output").html(errorMessage);
                    } else {
                        $("#errors-output").html(err.message);
                    }
                    $("#errors-output").css("background", "#f59d9d");
                    $("#errors-output").show();

                } else {

                    // At this point you may pass the token back to your server as you wish.
                    // In this example we append a hidden input to the form and submit it.                    
                    $("#paymentdev").show();
                    $(window).scrollTop($('#paymentdev').offset().top);                   
                    $("#paymentconfirmation").attr('action', checkoutAPIUrl);
                    $("#capturecontext").val(captureContext1);
                    $("#transienttoken").val(token);
                    $("#paymentconfirmation").submit();
                }
            });
        };
    </script>
    <div class="container card" id="cardcontainer1">
        <form id="paymentDetails" runat="server">
            <div class="card-body">
                <h1>Customer Details</h1>
                <input id="GenerateMicroForm"
                <div id="errorsDetails" role="alert" style="display: none; text-align: center" runat="server" class="errorMessage"></div>
                <div id="messagepannel" role="alert" style="display: none; text-align: center"></div>
                <div class="form-group">
                    <label>Forename</label>
                    <asp:TextBox runat="server" ID="Forename" CssClass="form-control" data-worldpay="forename" placeholder="Customer Forename" /><br />
                    <label>Surname</label>
                    <asp:TextBox runat="server" ID="Surname" CssClass="form-control" data-worldpay="surname" placeholder="Customer Surname" /><br />
                    <label>AddressLine1</label>
                    <asp:TextBox runat="server" ID="AddressLine1" CssClass="form-control" placeholder="Billing Address Line 1" /><br />
                    <label>City</label>
                    <asp:TextBox runat="server" ID="City" CssClass="form-control" placeholder="Billing Address City" /><br />
                    <label>Postcode</label>
                    <asp:TextBox runat="server" ID="Postcode" CssClass="form-control" placeholder="Billing Postcode" /><br />
                    <label>State</label>
                    <asp:TextBox runat="server" ID="State" CssClass="form-control" placeholder="State" /><br />
                    <label>Country</label>
                    <select id="Country" class="form-control" runat="server">
                        <option value="US">US</option>
                        <option value="IN">IN</option>
                    </select><br />
                    <label>Phone Number</label>
                    <asp:TextBox runat="server" ID="PhoneNumber" CssClass="form-control" placeholder="Your Phone Number" /><br />
                    <label>Email Address</label>
                    <asp:TextBox runat="server" ID="EmailAddress" CssClass="form-control" placeholder="Your Email Address" />  <br />
                 <div>
                      <asp:CheckBox runat="server" ID="SaveCardDetails" />
                    <label>Save Card Details</label>
                 </div>
                   
                </div>
                <asp:Button ID="Continue" runat="server" CssClass="btn btn-primary btn-block PlaceOrder" Text="Continue" OnClientClick="return btnclick();" OnClick="Continue_Click1" />
            </div>
        </form>
    </div>
    <div class="container card" id="cardcontainer" style="display: none;">
        <div class="card-body">
            <h1>Checkout</h1>
            <div id="errors-output" role="alert" style="display: none; text-align: center"></div>
            <form id="my-sample-form">
                <div id="carddiv" class="form-group">
                    <label for="cardholderName">Name</label>
                    <input id="cardholderName" class="form-control" name="cardholderName" placeholder="Name on the card">
                    <label id="cardNumber-label">Card Number</label>
                    <div id="number-container" class="form-control"></div>
                    <label for="securityCode-container">Security Code</label>
                    <div id="securityCode-container" class="form-control"></div>
                </div>

                <div class="form-row">
                    <div class="form-group col-md-6">
                        <label for="expMonth">Expiry month</label>
                        <select id="expMonth" class="form-control">
                            <option>01</option>
                            <option>02</option>
                            <option>03</option>
                            <option>04</option>
                            <option>05</option>
                            <option>06</option>
                            <option>07</option>
                            <option>08</option>
                            <option>09</option>
                            <option>10</option>
                            <option>11</option>
                            <option>12</option>
                        </select>
                    </div>
                    <div class="form-group col-md-6">
                        <label for="expYear">Expiry year</label>
                        <select id="expYear" class="form-control">
                            <option>2021</option>
                            <option>2022</option>
                            <option>2023</option>
                            <option>2024</option>
                            <option>2025</option>
                            <option>2026</option>
                            <option>2027</option>
                        </select>
                    </div>
                </div>

                <button type="button" id="pay-button" class="btn btn-primary btn-block PlaceOrder" onclick="payclick();">Pay</button>
                <input type="hidden" id="flexresponse" name="flexresponse">
            </form>
        </div>
    </div>
    <div class="container card">
        <div class="card-body" id="paymentdev" style="display: none;">
            <iframe name="my_iframe" class="form-control" style="height: 600px;"></iframe>
            <form id="paymentconfirmation"  method="post" target="my_iframe">
                <div id="UnsignedDataSection" class="section" style="display: none;">
                    <span>capture_context:</span><input id="capturecontext" type="text" name="capture_context"><br />
                    <span>transient_token:</span><input id="transienttoken" type="text" name="transient_token"><br />
                </div>
            </form>
        </div>
    </div>
</body>
</html>


//
//  CustomNativeView.m
//  CustomNativeView
//
//  Created by jjaychen on 2021/7/3.
//

#import "CustomNativeView.h"

@interface CustomNativeView ()
@property (nonatomic) UIView *ruleView;
@property (nonatomic) UIButton *ruleViewButton;
@end

@implementation CustomNativeView

-(void)toggle {
    [UIView animateWithDuration:1.0 animations:^{
        self.ruleView.alpha = 1.0 - self.ruleView.alpha;
    }];
}

-(void)exit {
    exit(0);
}

+(void)initExitAppButton {
//    static CustomNativeView * customNativeView = [[CustomNativeView alloc] init];
//    UIViewController *vc = UnityGetGLViewController();
//    UIButton *button = [[UIButton alloc] initWithFrame:CGRectZero];
//    [button setTitle:@"退出" forState:UIControlStateNormal];
//    [vc.view addSubview:button];
//    button.translatesAutoresizingMaskIntoConstraints = NO;
//    [NSLayoutConstraint activateConstraints:@[
//        [button.trailingAnchor constraintEqualToAnchor:vc.view.trailingAnchor constant:-16],
//        [button.bottomAnchor constraintEqualToAnchor:vc.view.bottomAnchor constant:-16],
//    ]];
//    [button addTarget:customNativeView action:@selector(exit) forControlEvents:UIControlEventTouchUpInside];
}

-(void)HideRuleViewAndButton {
    self.ruleView.alpha = 0;
    self.ruleViewButton.alpha = 0;
}

-(void)ShowRuleViewAndButton {
    self.ruleView.alpha = 1;
    self.ruleViewButton.alpha = 1;
}

+(void)initSceneTwoRuleView {
    static CustomNativeView * customNativeView = [[CustomNativeView alloc] init];
    if (customNativeView.ruleViewButton != nil || customNativeView.ruleView != nil) {
        return;
    }
    UIViewController *vc = UnityGetGLViewController();
    UIButton *button = [[UIButton alloc] initWithFrame:CGRectZero];
    [button setImage:[UIImage imageNamed:@"rule"] forState:UIControlStateNormal];
    [vc.view addSubview:button];
    button.translatesAutoresizingMaskIntoConstraints = NO;
    [NSLayoutConstraint activateConstraints:@[
        [button.trailingAnchor constraintEqualToAnchor:vc.view.trailingAnchor constant:-16],
        [button.topAnchor constraintEqualToAnchor:vc.view.topAnchor constant:130],
        [button.widthAnchor constraintEqualToConstant:45],
        [button.heightAnchor constraintEqualToConstant:45],
    ]];
    [button addTarget:customNativeView action:@selector(toggle) forControlEvents:UIControlEventTouchUpInside];
    customNativeView.ruleViewButton = button;
    
    UIVisualEffect *blurEffect = [UIBlurEffect effectWithStyle:UIBlurEffectStyleDark];
    UIVisualEffectView *visualEffectView = [[UIVisualEffectView alloc] initWithEffect:blurEffect];
    visualEffectView.alpha = 0;
    visualEffectView.clipsToBounds = YES;
    visualEffectView.layer.cornerRadius = 25.0;
    customNativeView.ruleView = visualEffectView;
    
    [vc.view addSubview:visualEffectView];
    visualEffectView.translatesAutoresizingMaskIntoConstraints = NO;
    [NSLayoutConstraint activateConstraints:@[
        [visualEffectView.topAnchor constraintEqualToAnchor:button.topAnchor],
        [visualEffectView.trailingAnchor constraintEqualToAnchor:button.leadingAnchor constant:-16],
    ]];
    
    UILabel *ruleTitleLabel = [[UILabel alloc] init];
    ruleTitleLabel.text = @"实验规范";
    ruleTitleLabel.font = [UIFont systemFontOfSize:20 weight:UIFontWeightBold];
    ruleTitleLabel.textColor = UIColor.whiteColor;
    ruleTitleLabel.numberOfLines = 0;
    [visualEffectView.contentView addSubview:ruleTitleLabel];
    
    ruleTitleLabel.translatesAutoresizingMaskIntoConstraints = NO;
    [NSLayoutConstraint activateConstraints:@[
        [ruleTitleLabel.leadingAnchor constraintEqualToAnchor:visualEffectView.leadingAnchor constant:24],
        [ruleTitleLabel.trailingAnchor constraintEqualToAnchor:visualEffectView.trailingAnchor constant:-24],
        [ruleTitleLabel.topAnchor constraintEqualToAnchor:visualEffectView.topAnchor constant:24]
    ]];
    
    
    UILabel *ruleLabel = [[UILabel alloc] init];
    ruleLabel.text = @"1. 点击模板、原料等试剂盒，移液枪会自动将试剂采样到试管中\n2. 打开PCR仪的盖子，将采样完的试管放入PCR仪中\n3. 设置程序和循环次数\n4. 等待运行结束，查看电泳结果";
    ruleLabel.font = [UIFont systemFontOfSize:18];
    ruleLabel.textColor = UIColor.whiteColor;
    ruleLabel.numberOfLines = 0;
    [visualEffectView.contentView addSubview:ruleLabel];
    
    ruleLabel.translatesAutoresizingMaskIntoConstraints = NO;
    [NSLayoutConstraint activateConstraints:@[
        [ruleLabel.leadingAnchor constraintEqualToAnchor:visualEffectView.leadingAnchor constant:24],
        [ruleLabel.trailingAnchor constraintEqualToAnchor:visualEffectView.trailingAnchor constant:-24],
        [ruleLabel.topAnchor constraintEqualToAnchor:ruleTitleLabel.bottomAnchor constant:8],
        [ruleLabel.bottomAnchor constraintEqualToAnchor:visualEffectView.bottomAnchor constant:-24],
        [ruleLabel.widthAnchor constraintLessThanOrEqualToConstant:350.0],
    ]];
    
    [customNativeView toggle];
    
    [[NSNotificationCenter defaultCenter] addObserver:customNativeView selector:@selector(ShowRuleViewAndButton) name:@"ShowRuleViewAndButton" object:nil];
    [[NSNotificationCenter defaultCenter] addObserver:customNativeView selector:@selector(HideRuleViewAndButton) name:@"HideRuleViewAndButton" object:nil];
}

extern "C" {
    void _initSceneTwoRuleView()
    {
        [CustomNativeView initSceneTwoRuleView];
    }
}

extern "C" {
    void _initExitAppButton() {
        [CustomNativeView initExitAppButton];
    }
}

extern "C" {
    void _postNotification(const char *name) {
        [[NSNotificationCenter defaultCenter] postNotificationName:[NSString stringWithUTF8String:name] object:nil userInfo:nil];
    }
}

@end

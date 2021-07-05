//
//  UnityToast.m
//  UnityToast
//
//  Created by jjaychen on 2021/7/2.
//

#import "UnityToast.h"

@implementation UnityToast

static UIView* _botToastView = nil;

static int imageIndex = 0;
static UIButton *_botButtonView = nil;

static UIView* _topToastView = nil;

static UIView* _bottomToastView = nil;

-(void)clearToastView {
    [_botToastView removeFromSuperview];
    _botButtonView = nil;
    [_topToastView removeFromSuperview];
    _topToastView = nil;
    [_bottomToastView removeFromSuperview];
    _bottomToastView = nil;
}

+(UIMenu *)getUIMenu {
    UIAction *actionOne = [UIAction actionWithTitle:@"实验背景" image:[UIImage systemImageNamed:@"1.circle.fill"] identifier:@"page0" handler:^(__kindof UIAction * _Nonnull action) {
        UnitySendMessage("MissionController", "SwitchMission", "1");
    }];

    UIAction *actionTwo = [UIAction actionWithTitle:@"DNA 体内复制" image:[UIImage systemImageNamed:@"2.circle.fill"] identifier:@"Page1" handler:^(__kindof UIAction * _Nonnull action) {
        UnitySendMessage("MissionController", "SwitchMission", "2");
    }];
    
    UIAction *actionThree = [UIAction actionWithTitle:@"任务一" image:[UIImage systemImageNamed:@"3.circle.fill"] identifier:@"Page11" handler:^(__kindof UIAction * _Nonnull action) {
        UnitySendMessage("MissionController", "SwitchMission", "3");
    }];
    
    UIAction *actionFour = [UIAction actionWithTitle:@"任务二" image:[UIImage systemImageNamed:@"4.circle.fill"] identifier:@"Page2" handler:^(__kindof UIAction * _Nonnull action) {
        UnitySendMessage("MissionController", "SwitchMission", "4");
        
    }];
    
    UIAction *actionFive = [UIAction actionWithTitle:@"任务三" image:[UIImage systemImageNamed:@"5.circle.fill"] identifier:@"Page3" handler:^(__kindof UIAction * _Nonnull action) {
        UnitySendMessage("MissionController", "SwitchMission", "5");
    }];
    
//    UIAction *actionSix = [UIAction actionWithTitle:@"Scene2" image:[UIImage systemImageNamed:@"6.circle.fill"] identifier:@"Scene2" handler:^(__kindof UIAction * _Nonnull action) {
//        UnitySendMessage("MissionController", "SwitchMission", "6");
//    }];
    
    UIAction *actionExit = [UIAction actionWithTitle:@"退出" image:[UIImage systemImageNamed:@"trash.fill"] identifier:nil handler:^(__kindof UIAction * _Nonnull action) {
        exit(0);
    }];
    actionExit.attributes = UIMenuElementAttributesDestructive;
    
    return [UIMenu menuWithTitle:@"" children:@[actionExit, actionFive, actionFour, actionThree, actionTwo, actionOne]];
}

+(void)initBotEmojiView {
    static UnityToast * unityToast = [[UnityToast alloc] init];
    
    UIImage *botImage0 = [UIImage imageNamed:@"Bot_0"];
    
    UIButton *button = [[UIButton alloc] init];
    button.menu = [UnityToast getUIMenu];
    button.showsMenuAsPrimaryAction = YES;
    [button setImage:botImage0 forState:UIControlStateNormal];
    [button setImage:botImage0 forState:UIControlStateHighlighted];
    _botButtonView = button;
    
    UIViewController *vc = UnityGetGLViewController();
    [vc.view addSubview:button];
    button.translatesAutoresizingMaskIntoConstraints = NO;
    [NSLayoutConstraint activateConstraints:@[
        [button.leadingAnchor constraintEqualToAnchor:vc.view.leadingAnchor constant:16],
        [button.bottomAnchor constraintEqualToAnchor:vc.view.bottomAnchor constant:-16],
        [button.widthAnchor constraintEqualToConstant:100],
        [button.heightAnchor constraintEqualToConstant:122],
    ]];
    
    [UnityToast botImageAnimator];
    
    [[NSNotificationCenter defaultCenter] addObserver:unityToast selector:@selector(clearToastView) name:@"ClearToastView" object:nil];
}

+(void)botImageAnimator {
    UIImage *botImage0 = [UIImage imageNamed:@"Bot_0"];
    UIImage *botImage1 = [UIImage imageNamed:@"Bot_1"];
    dispatch_time_t temp_time = dispatch_time(DISPATCH_TIME_NOW, (int64_t)(0.5 * NSEC_PER_SEC));
    dispatch_after(temp_time, dispatch_get_main_queue(), ^(void){
        if (_botToastView != nil) {
            if (imageIndex == 0) {
                [_botButtonView setImage:botImage1 forState:UIControlStateNormal];
                [_botButtonView setImage:botImage1 forState:UIControlStateHighlighted];
                imageIndex = 1;
            } else {
                imageIndex = 0;
                [_botButtonView setImage:botImage0 forState:UIControlStateNormal];
                [_botButtonView setImage:botImage0 forState:UIControlStateHighlighted];
            }
        }

        [UnityToast botImageAnimator];
    });
}

+(void)showBotToast:(NSString *)string with:(float) duration {
    UIViewController *vc = UnityGetGLViewController();

    UIVisualEffect *blurEffect = [UIBlurEffect effectWithStyle:UIBlurEffectStyleDark];
    UIVisualEffectView *visualEffectView = [[UIVisualEffectView alloc] initWithEffect:blurEffect];
    visualEffectView.clipsToBounds = YES;
    visualEffectView.layer.cornerRadius = 15.0;
    
    [vc.view addSubview:visualEffectView];
    visualEffectView.translatesAutoresizingMaskIntoConstraints = NO;
    [NSLayoutConstraint activateConstraints:@[
        [visualEffectView.leadingAnchor constraintEqualToAnchor:vc.view.leadingAnchor constant:116],
        [visualEffectView.bottomAnchor constraintEqualToAnchor:vc.view.bottomAnchor constant:-116],
    ]];
    
    UILabel *label = [[UILabel alloc] init];
    label.text = string;
    label.textColor = UIColor.whiteColor;
    label.numberOfLines = 0;
    [visualEffectView.contentView addSubview:label];
    
    label.translatesAutoresizingMaskIntoConstraints = NO;
    [NSLayoutConstraint activateConstraints:@[
        [label.leadingAnchor constraintEqualToAnchor:visualEffectView.leadingAnchor constant:24],
        [label.trailingAnchor constraintEqualToAnchor:visualEffectView.trailingAnchor constant:-24],
        [label.topAnchor constraintEqualToAnchor:visualEffectView.topAnchor constant:24],
        [label.bottomAnchor constraintEqualToAnchor:visualEffectView.bottomAnchor constant:-24],
        [label.widthAnchor constraintLessThanOrEqualToConstant:300.0],
        
    ]];
    
    if (_botToastView == nil) {
        _botToastView = visualEffectView;
        visualEffectView.alpha = 0;
        [UIView animateWithDuration:1 animations:^{
            visualEffectView.alpha = 1;
        }];
    } else {
        [_botToastView removeFromSuperview];
        _botToastView = visualEffectView;
    }
    
    dispatch_time_t popTime = dispatch_time(DISPATCH_TIME_NOW, (int64_t)(duration * NSEC_PER_SEC));
    dispatch_after(popTime, dispatch_get_main_queue(), ^(void){
        if (_botToastView == visualEffectView) {
            [UIView animateWithDuration:0.5 animations:^{
                visualEffectView.alpha = 0;
            } completion:^(BOOL finished) {
                if (finished) {
                    if (_botToastView == visualEffectView) {
                        _botToastView = nil;
                        [visualEffectView removeFromSuperview];
                    }
                }
            }];
        }
    });
}

+(void)showTopToast:(NSString *)string with:(float) duration {
    UIViewController *vc = UnityGetGLViewController();

    UIVisualEffect *blurEffect = [UIBlurEffect effectWithStyle:UIBlurEffectStyleDark];
    UIVisualEffectView *visualEffectView = [[UIVisualEffectView alloc] initWithEffect:blurEffect];
    visualEffectView.clipsToBounds = YES;
    visualEffectView.layer.cornerRadius = 15.0;
    
    [vc.view addSubview:visualEffectView];
    visualEffectView.translatesAutoresizingMaskIntoConstraints = NO;
    [NSLayoutConstraint activateConstraints:@[
        [visualEffectView.topAnchor constraintEqualToAnchor:vc.view.topAnchor constant:28],
        [visualEffectView.centerXAnchor constraintEqualToAnchor:vc.view.centerXAnchor],
    ]];
    
    UILabel *label = [[UILabel alloc] init];
    label.text = string;
    label.textColor = UIColor.whiteColor;
    label.numberOfLines = 0;
    [visualEffectView.contentView addSubview:label];
    
    label.translatesAutoresizingMaskIntoConstraints = NO;
    [NSLayoutConstraint activateConstraints:@[
        [label.leadingAnchor constraintEqualToAnchor:visualEffectView.leadingAnchor constant:24],
        [label.trailingAnchor constraintEqualToAnchor:visualEffectView.trailingAnchor constant:-24],
        [label.topAnchor constraintEqualToAnchor:visualEffectView.topAnchor constant:24],
        [label.bottomAnchor constraintEqualToAnchor:visualEffectView.bottomAnchor constant:-24],
        [label.widthAnchor constraintLessThanOrEqualToConstant:400.0],
        
    ]];
    
    if (_topToastView == nil) {
        _topToastView = visualEffectView;
        visualEffectView.alpha = 0;
        [UIView animateWithDuration:1 animations:^{
            visualEffectView.alpha = 1;
        }];
    } else {
        [_topToastView removeFromSuperview];
        _topToastView = visualEffectView;
    }
    
    dispatch_time_t popTime = dispatch_time(DISPATCH_TIME_NOW, (int64_t)(duration * NSEC_PER_SEC));
    dispatch_after(popTime, dispatch_get_main_queue(), ^(void){
        if (_topToastView == visualEffectView) {
            [UIView animateWithDuration:0.5 animations:^{
                visualEffectView.alpha = 0;
            } completion:^(BOOL finished) {
                if (finished) {
                    if (_topToastView == visualEffectView) {
                        _topToastView = nil;
                        [visualEffectView removeFromSuperview];
                    }
                }
            }];
        }
    });
}


+(void)showBottomToast:(NSString *)string with:(float) duration {
    UIViewController *vc = UnityGetGLViewController();

    UIVisualEffect *blurEffect = [UIBlurEffect effectWithStyle:UIBlurEffectStyleDark];
    UIVisualEffectView *visualEffectView = [[UIVisualEffectView alloc] initWithEffect:blurEffect];
    visualEffectView.clipsToBounds = YES;
    visualEffectView.layer.cornerRadius = 15.0;
    
    [vc.view addSubview:visualEffectView];
    visualEffectView.translatesAutoresizingMaskIntoConstraints = NO;
    [NSLayoutConstraint activateConstraints:@[
        [visualEffectView.bottomAnchor constraintEqualToAnchor:vc.view.bottomAnchor constant:-28],
        [visualEffectView.centerXAnchor constraintEqualToAnchor:vc.view.centerXAnchor],
    ]];
    
    UILabel *label = [[UILabel alloc] init];
    label.text = string;
    label.textColor = UIColor.whiteColor;
    label.numberOfLines = 0;
    [visualEffectView.contentView addSubview:label];
    
    label.translatesAutoresizingMaskIntoConstraints = NO;
    [NSLayoutConstraint activateConstraints:@[
        [label.leadingAnchor constraintEqualToAnchor:visualEffectView.leadingAnchor constant:24],
        [label.trailingAnchor constraintEqualToAnchor:visualEffectView.trailingAnchor constant:-24],
        [label.topAnchor constraintEqualToAnchor:visualEffectView.topAnchor constant:24],
        [label.bottomAnchor constraintEqualToAnchor:visualEffectView.bottomAnchor constant:-24],
        [label.widthAnchor constraintLessThanOrEqualToConstant:400.0],
        
    ]];
    
    if (_bottomToastView == nil) {
        _bottomToastView = visualEffectView;
        visualEffectView.alpha = 0;
        [UIView animateWithDuration:1 animations:^{
            visualEffectView.alpha = 1;
        }];
    } else {
        [_bottomToastView removeFromSuperview];
        _bottomToastView = visualEffectView;
    }
    
    dispatch_time_t popTime = dispatch_time(DISPATCH_TIME_NOW, (int64_t)(duration * NSEC_PER_SEC));
    dispatch_after(popTime, dispatch_get_main_queue(), ^(void){
        if (_bottomToastView == visualEffectView) {
            [UIView animateWithDuration:0.5 animations:^{
                visualEffectView.alpha = 0;
            } completion:^(BOOL finished) {
                if (finished) {
                    if (_bottomToastView == visualEffectView) {
                        _bottomToastView = nil;
                        [visualEffectView removeFromSuperview];
                    }
                }
            }];
        }
    });
}

+(void)showAlert:(NSString *)title message:(NSString *)message {
    UIAlertController *alert = [UIAlertController
                                alertControllerWithTitle:title
                                message:message
                                preferredStyle:UIAlertControllerStyleAlert];
    
    UIAlertAction* okButton = [UIAlertAction
                               actionWithTitle:@"OK"
                               style:UIAlertActionStyleDefault
                               handler:^(UIAlertAction * action) {
        
    }];
    
    [alert addAction:okButton];
    
    UIViewController *vc = UnityGetGLViewController();
    [vc presentViewController:alert animated:YES completion:nil];
}

extern "C" {
    void _initBotEmojiView()
    {
        [UnityToast initBotEmojiView];
    }
}

extern "C" {
    void _showBotToast(const char* string, float duration)
    {
        if (string) {
            [UnityToast showBotToast:[NSString stringWithUTF8String:string] with: duration];
        }
    }
}

extern "C" {
    void _showTopToast(const char* string, float duration)
    {
        if (string) {
            [UnityToast showTopToast:[NSString stringWithUTF8String:string] with: duration];
        }
    }
}

extern "C" {
    void _showBottomToast(const char* string, float duration)
    {
        if (string) {
            [UnityToast showBottomToast:[NSString stringWithUTF8String:string] with: duration];
        }
    }
}

extern "C" {
    void _showAlert(const char* title, const char* message)
    {
        if (title && message) {
            [UnityToast showAlert:[NSString stringWithUTF8String:title] message:[NSString stringWithUTF8String:message]];
        }
    }
}

@end
